using BackEnd;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class RankingViewer : MonoBehaviour
{
    [SerializeField]
    private GameObject frame;
    [SerializeField]
    private GameObject rankPrefab;
    [SerializeField]
    private Transform tfParent;

    [Header("PlayerRank")]
    [SerializeField]
    private RankingFrame playerRanking;

    private List<RankingFrame> rankList;
    public void RankingStart()
    {
        rankList = new List<RankingFrame>();

        for(int i = 0;i < 100; i++)
        {
            GameObject newRankingFrame = Instantiate(rankPrefab, tfParent);
            RankingFrame rankingFrame = newRankingFrame.GetComponent<RankingFrame>();
            rankingFrame.SetRanking();

            rankList.Add(rankingFrame);
        }

        frame.SetActive(false);
    }

    /// <summary>
    /// Ranking UI의 개인 랭킹 불러오기
    /// </summary>
    public void GetMyRank()
    {
        Backend.URank.User.GetMyRank(Constants.RANK_UUID, callback =>
        {
            string nickname = UserInfo.UserData.nickname == null ? UserInfo.UserData.gamerId : UserInfo.UserData.nickname;

            if(callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData rankData = callback.FlattenRows();

                    if(rankData.Count <= 0)
                    {
                        // 데이터에 없는 경우
                        playerRanking.Rank = -1;
                        playerRanking.Level = -1;
                        playerRanking.Nickname = "찾을 수 없는 유저";
                    }
                    else
                    {
                        playerRanking.Rank = int.Parse(rankData[0]["rank"].ToString());
                        playerRanking.Level = int.Parse(rankData[0]["score"].ToString());
                        playerRanking.Nickname = rankData[0].ContainsKey("nickname") == true ? rankData[0]["nickname"]?.ToString() : UserInfo.UserData.gamerId;
                    }
                }
                catch (System.Exception e)
                {
                    playerRanking.Rank = -1;
                    playerRanking.Level = -1;
                    playerRanking.Nickname = "찾을 수 없는 유저";

                    Debug.LogError("GetMyRank() error : " + e);
                }
            }
        });
    }


    public void SetRankings()
    {
         Backend.URank.User.GetRankList(Constants.RANK_UUID, 100, callback => {
             if (callback.IsSuccess())
             {
                 // Json데이터 파싱 성공
                 try {

                     LitJson.JsonData rankData = callback.FlattenRows();

                     if(rankData.Count <= 0)
                     {
                         for (int i = 0; i < 100; i++)
                             rankList[i].SetRanking();

                         Debug.Log("데이터가 존재하지 않습니다.");
                         BackendRank.Instance.RankInsert();
                     }
                     else
                     {
                         
                         int rankCount = rankData.Count ;

                         for(int i = 0;i < rankCount; i++)
                         {
                             rankList[i].Rank = int.Parse(rankData[i]["rank"].ToString());
                             rankList[i].Level = int.Parse(rankData[i]["score"].ToString());
                             rankList[i].Nickname = rankData[i].ContainsKey("nickname") == true ? rankData[i]["nickname"]?.ToString() : UserInfo.UserData.gamerId;
                         }
                     }
                 }
                 catch (System.Exception e){
                     Debug.Log("실패 : " + e);
                 };
             }
             else
             {
                 Manager.Instance.UIAndScene.Notion.SetNotionText($"랭크 조회 중, 오류가 발생 했습니다. {callback}");
             }
        });
    }
}

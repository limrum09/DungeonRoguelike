using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Text;

public class BackendRank : MonoBehaviour
{
    private static BackendRank instance;

    public static BackendRank Instance
    {
        get
        {
            if (instance == null)
            {
                // Scene에서 BackendManager 객체를 찾는다.
                instance = FindObjectOfType<BackendRank>();

                // 없다면 새로운 GameObject를 생성하고 BackendManager를 추가한다.
                if (instance == null)
                {
                    GameObject obj = new GameObject("BackendRankManager");
                    instance = obj.AddComponent<BackendRank>();
                }
            }
            return instance;
        }
    }

    public void RankInsert()
    {
        // 로그인이 되어 있는지 확인
        if (!Backend.IsLogin)
            return;

        string rowIndate = string.Empty;

        // 랭킹 데이터
        Param param = new Param();
        param.Add("Level", Manager.Instance.Game.Level);

        Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback => {
            if (callback.IsSuccess())
            {
                // 데이터가 존대하는 경우
                if (callback.FlattenRows().Count > 0)
                {
                    rowIndate = callback.FlattenRows()[0]["inDate"].ToString();
                }
                // 데이터가 없는 경우
                else
                {
                    var bro = Backend.GameData.Insert(Constants.USER_DATA_TABLE);

                    if(bro.IsSuccess() == false)
                    {
                        Debug.Log("데이터 삽입 실패");
                        return;
                    }

                    rowIndate = bro.GetInDate();
                }

                var rankBro = Backend.URank.User.UpdateUserScore(Constants.RANK_UUID, Constants.USER_DATA_TABLE, rowIndate, param);

                if (rankBro.IsSuccess())
                {
                    Manager.Instance.UIAndScene.Notion.SetNotionText("랭킹 삽입에 성공했습니다.");
                }
                else
                {
                    Manager.Instance.UIAndScene.Notion.SetNotionText("랭킹 삽입에 실패했습니다. " + rankBro);
                }                    
            }
            else
            {
                Debug.Log("데이터 조회 실패");
                return;
            }
        });
    }
}

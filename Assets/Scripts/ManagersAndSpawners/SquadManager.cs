using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquadManager : MonoBehaviour
{
    [SerializeField] private GameObject leader;
    
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> squad { get; } = new List<GameObject>();
    void Start()
    {
        AddLeader();
    }

    void AddLeader()
    {
        //GameObject leader = Instantiate(prefabs[0], transform.position, transform.rotation, transform);
        squad.Add(leader);
    }
    
    public void AddMember()
    {
        GameObject member = Instantiate(prefabs[1], squad[squad.Count - 1].transform.position + new Vector3(-2,0, 0), squad[squad.Count - 1].transform.rotation, transform);
        member.GetComponent<SquadMemberController>().id = squad.Count;
        squad.Add(member);
    }
    
    public void RemoveMember()
    {
        if (squad.Count == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Destroy(squad[squad.Count - 1]);
        squad.RemoveAt(squad.Count - 1);
    }

    public void RemoveMemberAt(int id)
    {
        Destroy(squad[id]);
        squad.RemoveAt(id);

        for (int i = 1; i < squad.Count; i++)
        {
            squad[i].GetComponent<SquadMemberController>().id = i;
        }
    }
}

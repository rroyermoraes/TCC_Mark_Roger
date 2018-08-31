
using UnityEngine;

public class CaseNode : MonoBehaviour {
    public int id;
    public void CallLink() {
        FindObjectOfType<Link>().TestRightClick(this.gameObject);
    }
}

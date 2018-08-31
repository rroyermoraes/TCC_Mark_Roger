
using UnityEngine;
using UnityEngine.UI;

public class UIMove : MonoBehaviour {
    private Rect r;
    private Vector3 offset = new Vector3();
    private Vector2 desiredPos = new Vector2();

    private void Start()
    {
        r = this.GetComponent<RectTransform>().rect;
    }

    public void Move() {
        desiredPos = new Vector2(Input.mousePosition.x - offset.x, Input.mousePosition.y - offset.y);

        if (desiredPos.x > r.width && desiredPos.x < Screen.width - r.width) {
            this.transform.position = new Vector3(desiredPos.x,transform.position.y,0);
        }
        if ( desiredPos.y > r.height && desiredPos.y < Screen.height - r.height)
        {
            this.transform.position = new Vector3(transform.position.x, desiredPos.y, 0);
        }



    }
    public void SetOffset() {
        offset = new Vector3(Input.mousePosition.x - this.transform.position.x, Input.mousePosition.y - this.transform.position.y, 0);
    }
    public void SetTopSibling() {
        transform.SetAsLastSibling();
    }

}

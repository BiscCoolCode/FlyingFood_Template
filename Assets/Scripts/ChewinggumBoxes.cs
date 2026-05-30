using UnityEngine;

public class ChewinggumBoxes : MonoBehaviour
{
    [SerializeField] private GameObject _BoxBlue;
    [SerializeField] private GameObject _BoxGreen;
    [SerializeField] private GameObject _BoxPurple;

    private GameObject[] _chewinggumBoxes = new GameObject[3];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _chewinggumBoxes[0] = Instantiate(_BoxBlue);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = transform.position + new Vector3(2, 0.5f, 1);
        _chewinggumBoxes[0].transform.position = Vector3.Lerp(_chewinggumBoxes[0].transform.position, targetPos, 0.1f);
    }
}

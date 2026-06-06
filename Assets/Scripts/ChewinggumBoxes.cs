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
        _chewinggumBoxes[1] = Instantiate(_BoxGreen);
        _chewinggumBoxes[2] = Instantiate(_BoxPurple);

        _chewinggumBoxes[0].SetActive(false);
        _chewinggumBoxes[1].SetActive(false);
        _chewinggumBoxes[2].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreManager.Instance.Score >= 25)
        {
            Vector3 targetPos = transform.position + new Vector3(2, 0.5f, 1);
            _chewinggumBoxes[0].transform.position = Vector3.Lerp(_chewinggumBoxes[0].transform.position, targetPos, 0.1f);
            _chewinggumBoxes[0].SetActive(true);
        }

        if (ScoreManager.Instance.Score >= 50)
        {
            Vector3 targetPos1 = transform.position + new Vector3(-2, 0.5f, 1);
            _chewinggumBoxes[1].transform.position = Vector3.Lerp(_chewinggumBoxes[1].transform.position, targetPos1, 0.1f);
            _chewinggumBoxes[1].SetActive(true);
        }

        if (ScoreManager.Instance.Score >= 100)
        {
            Vector3 targetPos2 = transform.position + new Vector3(0, 0.5f, -2);
            _chewinggumBoxes[2].transform.position = Vector3.Lerp(_chewinggumBoxes[2].transform.position, targetPos2, 0.1f);
            _chewinggumBoxes[2].SetActive(true);
        }
    }
}

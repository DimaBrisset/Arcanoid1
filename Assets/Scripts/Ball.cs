using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Variables

    [Header("Movements")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _startDirection;
    [SerializeField] private Pad _pad;

    [Header("Sprites")]
    [SerializeField] private Sprite _circleBlockDamageSmall;
    [SerializeField] private Sprite _circleBlockDamageBig;
    [SerializeField] private Sprite _rectangleBlockDamageSmall;
    [SerializeField] private Sprite _rectangleBlockDamageBig;

    [Header("Text")]
    [SerializeField] private TextMeshPro _calcClickLable;

    private int _countPoints;

    #endregion


    #region Unity lifecycle

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _startDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3) _rb.velocity);
    }

    #endregion


    #region Public Methods

    public void StartMove()
    {
        _rb.velocity = _startDirection;
    }

    public void MoveWithPad()
    {
        Vector3 padPosition = _pad.transform.position;
        Vector3 currentPosition = transform.position;
        currentPosition.x = padPosition.x;
        transform.position = currentPosition;
    }

    public void CountPoints(int points)
    {
        _countPoints += points;

        SetCalculatedStep($"Очки: {_countPoints}");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Lose"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        if (col.gameObject.CompareTag("Block"))
        {
            Destroy(col.gameObject);
            CountPoints(1);
        }

        if (col.gameObject.CompareTag("BlockCrash"))
        {
            col.gameObject.tag = "BlockCrash";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _circleBlockDamageBig;
            CountPoints(2);
        }

        if (col.gameObject.CompareTag("BlockCrash"))
        {
            col.gameObject.tag = "BlockCrash";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _circleBlockDamageSmall;
            CountPoints(3);
        }

        if (col.gameObject.CompareTag("BlockRectangle"))
        {
            col.gameObject.tag = "BlockRectangle";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _rectangleBlockDamageSmall;
            CountPoints(4);
        }

        if (col.gameObject.CompareTag("BlockRectangle"))
        {
            col.gameObject.tag = "BlockRectangle";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _rectangleBlockDamageBig;
            CountPoints(5);
        }
    }

    #endregion


    #region Priveat Methods

    private void SetCalculatedStep(string text)
    {
        _calcClickLable.text = text;
    }

    #endregion
}
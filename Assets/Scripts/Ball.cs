using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _startDirection;
    [SerializeField] private Pad _pad;

    [SerializeField] private Sprite _damageSmall;
    [SerializeField] private Sprite _damageBig;

    [SerializeField] private int _countPoints;

    [SerializeField] private TextMeshPro _calcClickLable;

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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Lose"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        if (col.gameObject.CompareTag("Block"))
        {
            Destroy(col.gameObject);
            CountPoints(15);
        }

        if (col.gameObject.CompareTag("BlockCircle"))
        {
            col.gameObject.tag = "BlockCircle";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _damageBig;
            CountPoints(10);
        }

        if (col.gameObject.CompareTag("BlockCircle2"))
        {
            col.gameObject.tag = "BlockCircle";
            col.gameObject.tag = "Block";
            col.gameObject.GetComponent<SpriteRenderer>().sprite = _damageSmall;
            CountPoints(5);
        }
    }

    public void CountPoints(int points)
    {
        _countPoints += points;

        SetCalculatedStep($"Очки: {_countPoints}");
    }

    private void SetCalculatedStep(string text)
    {
        _calcClickLable.text = text;
    }
}
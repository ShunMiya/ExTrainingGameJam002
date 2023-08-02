using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private AudioSource auso;
    [SerializeField] private AudioClip move;
    [SerializeField] private float volume;

    private Vector3 previousPosition; // 前回のフレームの位置
    [SerializeField] private float SEdis;

    private void Start()
    {
        auso = GetComponent<AudioSource>();
        previousPosition = transform.position; // 初期位置を記録
    }

    void Update()
    {
        // WASDキーの入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 移動量を計算
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        // 現在位置に移動量を加算して移動
        transform.position += movement;


        if (Vector3.Distance(transform.position, previousPosition) > SEdis)
        {
            auso.PlayOneShot(move, volume);
            // 現在位置を前回のフレームの位置として記録
            previousPosition = transform.position;
        }
    }
}
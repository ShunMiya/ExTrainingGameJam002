using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // Rigidbody2Dの参照を保持する変数

    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
        // もしRigidbody2Dがアタッチされていない場合、エラーメッセージを表示してスクリプトを無効化する
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the Player game object.");
            enabled = false; // スクリプトを無効化して移動処理が行われないようにする
        }
    }

    void Update()
    {
        // WASDキーの入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 移動量を計算
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;

        // Rigidbody2Dを取得し、移動を適用
        rb.MovePosition(rb.position + movement);

    }
}
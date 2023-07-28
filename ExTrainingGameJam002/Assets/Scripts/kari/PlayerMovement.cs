using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // Rigidbody2D�̎Q�Ƃ�ێ�����ϐ�

    void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody2D>();
        // ����Rigidbody2D���A�^�b�`����Ă��Ȃ��ꍇ�A�G���[���b�Z�[�W��\�����ăX�N���v�g�𖳌�������
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the Player game object.");
            enabled = false; // �X�N���v�g�𖳌������Ĉړ��������s���Ȃ��悤�ɂ���
        }
    }

    void Update()
    {
        // WASD�L�[�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �ړ��ʂ��v�Z
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;

        // Rigidbody2D���擾���A�ړ���K�p
        rb.MovePosition(rb.position + movement);

    }
}
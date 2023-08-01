using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private AudioSource auso;
    [SerializeField] private AudioClip move;
    [SerializeField] private float volume;

    private Vector3 previousPosition; // �O��̃t���[���̈ʒu
    [SerializeField] private float SEdis;

    private void Start()
    {
        auso = GetComponent<AudioSource>();
        previousPosition = transform.position; // �����ʒu���L�^
    }

    void Update()
    {
        // WASD�L�[�̓��͂��擾
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �ړ��ʂ��v�Z
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        // ���݈ʒu�Ɉړ��ʂ����Z���Ĉړ�
        transform.position += movement;


        if (Vector3.Distance(transform.position, previousPosition) > SEdis)
        {
            auso.PlayOneShot(move, volume);
            // ���݈ʒu��O��̃t���[���̈ʒu�Ƃ��ċL�^
            previousPosition = transform.position;
        }
    }
}
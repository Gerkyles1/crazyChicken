using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float detectionDistance = 15f;
    private float moveSpeed = 8f;
    private bool die = false;

    void FixedUpdate()
    {
        if (!die)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerMovement.playerTransform.position);
            if (PlayerMovement.IsPlayerDie())
            {
                GetComponent<Animator>().ResetTrigger("run");
                GetComponent<Animator>().SetTrigger("playerLose");
                return;
            }
            else if (distanceToPlayer <= detectionDistance)
            {
                transform.parent = null;

                // �������� �������� �� ������
                Vector3 direction = (PlayerMovement.playerTransform.position - transform.position).normalized;

                // ³������, �� ��� ������ ���������� ������ �� ������
                float offsetDistance = 0.25f;

                // �������� ���� ������� ������, ���������� �� ������ �� ����� �������
                Vector3 targetPosition = PlayerMovement.playerTransform.position - direction * offsetDistance;

                // ������ ������ � �������� ���� �������
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // ���������� ������� ������ �� ������
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // ����������� ������� �� ������ � ��������
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // ������������ ������ ������� ���
                GetComponent<Animator>().SetTrigger("run");
            }
        }
        else
        {
            GetComponent<Animator>().ResetTrigger("run");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            die = true;
            GetComponent<Animator>().SetTrigger("die");
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.5f);
        }
    }
}

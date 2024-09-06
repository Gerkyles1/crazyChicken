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

                // Отримуємо напрямок до гравця
                Vector3 direction = (PlayerMovement.playerTransform.position - transform.position).normalized;

                // Відстань, на яку будуть зміщуватись вороги від гравця
                float offsetDistance = 0.25f;

                // Отримуємо нову позицію ворога, відступаючи від гравця на певну відстань
                Vector3 targetPosition = PlayerMovement.playerTransform.position - direction * offsetDistance;

                // Рухаємо ворога в напрямку нової позиції
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Обчислюємо поворот ворога до гравця
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // Застосовуємо поворот до ворога з плавністю
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // Встановлюємо тригер анімації бігу
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

using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); // Точки маршрута
    public float moveSpeed = 5f; // Скорость движения
    public float turnSpeed = 2f; // Скорость поворота
    public float driftIntensity = 1f; // Интенсивность "скольжения"
    public float waypointTolerance = 1f; // Радиус достижения точки

    private int currentWaypointIndex = 0;

    public bool canGo = false;

    void FixedUpdate()
    {
        if (!canGo) return;
        if (waypoints.Count == 0) return;

        // Текущая цель
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        // Рассчитываем направление
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Поворот к цели
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        // Имитация дрифта
        Vector3 driftOffset = transform.right * Mathf.Sin(Time.time * driftIntensity) * 0.1f;

        // Перемещение вперед с учетом дрифта
        transform.position += (transform.forward + driftOffset) * moveSpeed * Time.deltaTime;

        // Проверяем достижение текущей точки
        if (Vector3.Distance(transform.position, targetPosition) < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}
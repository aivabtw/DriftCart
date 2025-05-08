using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>(); // ����� ��������
    public float moveSpeed = 5f; // �������� ��������
    public float turnSpeed = 2f; // �������� ��������
    public float driftIntensity = 1f; // ������������� "����������"
    public float waypointTolerance = 1f; // ������ ���������� �����

    private int currentWaypointIndex = 0;

    public bool canGo = false;

    void FixedUpdate()
    {
        if (!canGo) return;
        if (waypoints.Count == 0) return;

        // ������� ����
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;

        // ������������ �����������
        Vector3 direction = (targetPosition - transform.position).normalized;

        // ������� � ����
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        // �������� ������
        Vector3 driftOffset = transform.right * Mathf.Sin(Time.time * driftIntensity) * 0.1f;

        // ����������� ������ � ������ ������
        transform.position += (transform.forward + driftOffset) * moveSpeed * Time.deltaTime;

        // ��������� ���������� ������� �����
        if (Vector3.Distance(transform.position, targetPosition) < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}
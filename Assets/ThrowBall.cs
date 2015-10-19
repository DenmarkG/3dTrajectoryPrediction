/*
THIS WORKS, DON'T TOUCH IT
*/
using UnityEngine;
using System.Collections;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private float m_throwPower = 100f;
    [SerializeField] private GameObject m_target = null;

    Vector3 m_initialVelocity = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                m_target.transform.position = hit.point + (Vector3.up * (m_target.GetComponent<BoxCollider>().bounds.max.y / 2));
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_initialVelocity = CalculateInitialVelocity();
            this.GetComponent<Rigidbody>().velocity = m_initialVelocity;            
        }
    }

    private Vector3 CalculateInitialVelocity()
    {
        Vector3 initialVelocity = Vector3.zero;
        float gravity = Mathf.Abs(Physics.gravity.y);

        Vector3 dirToTarget = m_target.transform.position - this.transform.position;
        float range = dirToTarget.magnitude;
        dirToTarget.Normalize();
        //float angle = CalculateAngleToHitTarget(range, dirToTarget.y);

        float flightTime = range / m_throwPower;
        float yVel = .5f * gravity * flightTime;

        initialVelocity = new Vector3(dirToTarget.x * m_throwPower, yVel, dirToTarget.z * m_throwPower);
        
        return initialVelocity;
    }

    float CalculateAngleToHitTarget(float targetRange, float yDelta)
    {
        float angle = 45;
        float gravity = Mathf.Abs(Physics.gravity.y);

        float numerator, denominator;

        float b = m_throwPower * m_throwPower;

        float determinant = Mathf.Pow(m_throwPower, 4) - ((gravity * gravity) * (targetRange * targetRange));    

        if (determinant > 0)
        {
            float root = Mathf.Sqrt(determinant);
            float posNumerator = b + root;
            float NegNumerator = b - root;

            numerator = (posNumerator < NegNumerator) ? posNumerator : NegNumerator;
            denominator = gravity * targetRange;
            angle = Mathf.Atan(numerator / denominator);

            angle = (angle * 180) / Mathf.PI;

            Debug.Log("Angle: " + angle);
        }

        return angle;
    }

}

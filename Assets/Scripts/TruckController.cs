using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    // Define an enum for the types of rubbish
    public enum RubbishType
    {
        Plastic,
        Crystal,
        Paper,
        Residual
    }

    // Define an enum for the types of trucks
    public enum TruckType
    {
        Blue,
        Green,
        Orange,
        Yellow
    }

    public TruckType truckType; // Set this in the Inspector for each truck

    private List<Rubbish> attachedRubbishList = new List<Rubbish>(); // List to hold multiple pieces of rubbish

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rubbish"))
        {
            Rubbish rubbish = other.GetComponent<Rubbish>();
            if (rubbish != null && CanPickUpRubbish(rubbish.rubbishType))
            {
                AttachRubbish(rubbish);

                // Disable the collider for the collected rubbish
                Collider rubbishCollider = rubbish.GetComponent<Collider>();
                if (rubbishCollider != null)
                {
                    rubbishCollider.enabled = false;
                }
            }
        }
        else if (other.CompareTag("Bin") && attachedRubbishList.Count > 0)
        {
            Bin bin = other.GetComponent<Bin>();
            if (bin != null && CanDepositRubbish(bin))
            {
                DepositRubbish(bin.transform);
            }
        }
    }

    bool CanPickUpRubbish(RubbishType rubbishType)
    {
        switch (truckType)
        {
            case TruckType.Yellow:
                return rubbishType == RubbishType.Plastic;
            case TruckType.Green:
                return rubbishType == RubbishType.Crystal;
            case TruckType.Blue:
                return rubbishType == RubbishType.Paper;
            case TruckType.Orange:
                return rubbishType == RubbishType.Residual;
            default:
                return false;
        }
    }

    bool CanDepositRubbish(Bin bin)
    {
        if (attachedRubbishList.Count == 0)
            return false;

        RubbishType rubbishType = attachedRubbishList[0].rubbishType;
        switch (rubbishType)
        {
            case RubbishType.Plastic:
                return bin.binType == Bin.BinType.Yellow;
            case RubbishType.Crystal:
                return bin.binType == Bin.BinType.Green;
            case RubbishType.Paper:
                return bin.binType == Bin.BinType.Blue;
            case RubbishType.Residual:
                return bin.binType == Bin.BinType.Orange;
            default:
                return false;
        }
    }

    void AttachRubbish(Rubbish rubbish)
    {
        attachedRubbishList.Add(rubbish);

        // Set the rubbish object as a child of the truck
        rubbish.transform.SetParent(this.transform);

        // Disable the rubbish's Rigidbody to prevent physics interactions
        Rigidbody rubbishRb = rubbish.GetComponent<Rigidbody>();
        if (rubbishRb != null)
        {
            rubbishRb.isKinematic = true;
        }

        // Set the position of the rubbish to be at y = 8.5 + (index * offset) in the truck's local space
        float yOffset = 8.5f + (attachedRubbishList.Count - 1) * 1.5f; // Example offset for stacking
        rubbish.transform.localPosition = new Vector3(0, yOffset, 0);

        // Adjust the scale of the rubbish to make it appear smaller on the truck (dividing by 2)
        rubbish.transform.localScale *= 0.5f;

        Debug.Log("List length: " + attachedRubbishList.Count);
    }

    void DepositRubbish(Transform binTransform)
    {
        // Detach and process each piece of rubbish
        foreach (var rubbish in attachedRubbishList)
        {
            // Debug.Log("Depositing rubbish: " + rubbish.rubbishType);

            rubbish.transform.SetParent(null);

            // Restore the original size of the rubbish
            rubbish.transform.localScale *= 2;

            // Set the position of the rubbish to the bin's position
            rubbish.transform.position = binTransform.position;

            // Enable the rubbish's Rigidbody to interact with physics again
            Rigidbody rubbishRb = rubbish.GetComponent<Rigidbody>();
            if (rubbishRb != null)
            {
                rubbishRb.isKinematic = false;
            }

            // Destroy the rubbish after 0.5 seconds
            Destroy(rubbish.gameObject, .5f);

            // Notify the CounterController to increment the counter
            CounterController.Instance.IncrementCounter();
        }

        // Clear the attached rubbish list
        attachedRubbishList.Clear();
    }
}

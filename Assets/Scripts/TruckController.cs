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

    private Rubbish attachedRubbish;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rubbish"))
        {
            Rubbish rubbish = collision.gameObject.GetComponent<Rubbish>();
            if (rubbish != null && CanPickUpRubbish(rubbish.rubbishType) && attachedRubbish == null)
            {
                AttachRubbish(rubbish);
            }
        }
        else if (collision.gameObject.CompareTag("Bin") && attachedRubbish != null)
        {
            Bin bin = collision.gameObject.GetComponent<Bin>();
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
        switch (attachedRubbish.rubbishType)
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
        attachedRubbish = rubbish;

        // Set the rubbish object as a child of the truck
        rubbish.transform.SetParent(this.transform);

        // Disable the rubbish's Rigidbody to prevent physics interactions
        Rigidbody rubbishRb = rubbish.GetComponent<Rigidbody>();
        if (rubbishRb != null)
        {
            rubbishRb.isKinematic = true;
        }

        // Set the position of the rubbish to be at y = 8.5 in the truck's local space
        rubbish.transform.localPosition = new Vector3(0, 8.5f, 0);

        // Adjust the scale of the rubbish to make it appear smaller on the truck (dividing by 2)
        rubbish.transform.localScale *= 0.5f;
    }

    void DepositRubbish(Transform binTransform)
    {
        // Detach the rubbish from the truck
        attachedRubbish.transform.SetParent(null);

        // Restore the original size of the rubbish
        attachedRubbish.transform.localScale *= 2;

        // Set the position of the rubbish to the bin's position
        attachedRubbish.transform.position = binTransform.position;

        // Enable the rubbish's Rigidbody to interact with physics again
        Rigidbody rubbishRb = attachedRubbish.GetComponent<Rigidbody>();
        if (rubbishRb != null)
        {
            rubbishRb.isKinematic = false;
        }

        // Clear the attached rubbish
        attachedRubbish = null;
    }
}

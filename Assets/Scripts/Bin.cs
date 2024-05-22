using UnityEngine;

public class Bin : MonoBehaviour
{
    // Define an enum for the types of bins
    public enum BinType
    {
        Blue,
        Green,
        Orange,
        Yellow
    }

    public BinType binType; // Set this in the Inspector for each bin
}

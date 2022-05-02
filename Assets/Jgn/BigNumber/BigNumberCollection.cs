using UnityEngine;

[CreateAssetMenu(fileName = "BigNumberCollection", menuName = "New BigNumberCollection", order = 0)]
public class BigNumberCollection : ScriptableObject
{
    public BigNumber[] _bigNumbers;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SCRIPTABLE OBJECT BASE CLASS
/// USED TO GENERATE SO WITHIN THE PROJECT
/// </summary>

[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]
public class RandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}

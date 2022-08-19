using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SCRIPTABLE OBJECT BASE CLASS
/// USED TO GENERATE SO WITHIN THE PROJECT
/// CUSTOM CLASS, PROVIDED FOR YOU
/// </summary>

[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "PCG/RandomWalkData")]
public class RandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    
    // Start in a random position somewhere on the existing floor tiles
    // as opposed to always starting in the same position
    public bool startRandomlyEachIteration = true;
}

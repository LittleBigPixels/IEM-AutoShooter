// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Game
// {
//     private List<PNJ> pnjs;
//
//     void Update()
//     {
//         foreach (PNJ pnj in pnjs)
//         {
//             pnj.Act();
//         }
//     }
// }
//
// public interface IPNJ
// {
//     void Act();
//     void DoSomethingWhenDying();
// }
//
// public interface ITarget
// {
//     void ApplyDamage(int damage);
// }
//
// public abstract class PNJ
// {
//     public string Name;
//     public int Health;
//
//     public void Logger(String message)
//     {
//         Debug.Log(message);
//     }
//
//     public abstract void Act();
// }
//
// public class GuardPNJ : MonoBehaviour, IPNJ, ITarget
// {
//     public void Act()
//     {
//         //Patrol city
//     }
//
//     public void DoSomethingWhenDying()
//     {
//         throw new NotImplementedException();
//     }
//
//     public void ApplyDamage(int damage)
//     {
//     }
// }
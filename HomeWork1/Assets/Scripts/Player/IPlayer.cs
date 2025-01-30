using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IPlayer
    {
        int Health { get; set; }
        Transform Transform { get; }
        bool IsPlayer { get; }

    }
}
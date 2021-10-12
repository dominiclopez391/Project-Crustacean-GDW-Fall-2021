using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Character
{
    public float WALKING_ACCELERATION;
    public float GRAVITY_ACCELERATION;
    public float WALK_MAX_SPEED;
    public float COYOTE_JUMP_TIME;
    public float JUMP_VEL; //jump height
    public float JUMP_STALL;
};

public abstract class CharacterSettings : MonoBehaviour
{
    public abstract Character GetSettingsFor();
}

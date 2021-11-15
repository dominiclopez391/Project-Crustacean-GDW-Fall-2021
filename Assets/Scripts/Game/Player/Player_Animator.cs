using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    Animator anim;
    GameObject jumpPrefab, landPrefab, wallClingPrefab, wallJumpPrefab, dashStartPrefab, dashContinuePrefab;
    GameObject player;
    private const float PARTICLE_SYSTEM_DEPSPAWN_TIMER = 3.0f;
    private const float landingOffsetX = 0.2f, landingOffsetY = 0.046875f, fastFallOffset = -0.1f, jumpOffsetX = 0.1f, jumpOffsetY = -0.1f, 
        wallClingOffsetX = 0.05f, wallClingOffsetY = 0.2f, wallJumpOffset = 0.1f, dashStartOffsetX = 0.2f, dashStartOffsetY = 0.0937f, dashContinueOffsetY = 0.03f;
    private float tWallClingAnim = 0f, wallClingLoopTime = 0.1f; //time for repeating the wall clinging animation
    private float tDashContAnim = 0f, dashContLoopTime = 0.01f; // time for repeating the dash continuing animation


    public Player_Animator Initialize(Animator anim, GameObject jPf, GameObject lPf, GameObject wcPf, GameObject wjPf, GameObject dsPf, GameObject dcPf, GameObject p)
    {
        this.anim = anim;
        jumpPrefab = jPf;
        landPrefab = lPf;
        wallClingPrefab = wcPf;
        wallJumpPrefab = wjPf;
        dashStartPrefab = dsPf;
        dashContinuePrefab = dcPf;
        player = p;
        return this;
    }

    public void createLandingParticle(float slopeDegrees)
    {
        //Creates two landing particle animations left and right of the collision spot
        //This gives a visual effect of a fall impact with the ground
        Vector2 landingPosLeft = Quaternion.Euler(0, 0, slopeDegrees) * 
            (new Vector3(player.transform.position.x - landingOffsetX, player.transform.position.y - landingOffsetY, 0) - player.transform.position) + player.transform.position;
        Vector2 landingPosRight = Quaternion.Euler(0, 0, slopeDegrees) * 
            (new Vector3(player.transform.position.x + landingOffsetX, player.transform.position.y - landingOffsetY, 0) - player.transform.position) + player.transform.position;
        // rotate vector forward by 60 degrees Quaternion.Euler(60, 0, 0) * Vector3.forward
        // rotate a point around a pivot  Quaternion.Euler(0,0,slopeDegrees) * (new Vector3(player.transform.position.x - landingOffsetX, player.transform.position.y - landingOffsetY, 0) - player.transform.position) + player.transform.position;
        GameObject landingSpotLeft = Instantiate(landPrefab, landingPosLeft, Quaternion.Euler(0, 0, 0));
        landingSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        ParticleSystem.MainModule psMainLeft = landingSpotLeft.GetComponent<ParticleSystem>().main;
        psMainLeft.startRotation = - Mathf.Deg2Rad * slopeDegrees; 
        
        GameObject landingSpotRight = Instantiate(landPrefab, landingPosRight, Quaternion.Euler(0, 0, 0));
        ParticleSystem.MainModule psMainRight = landingSpotRight.GetComponent<ParticleSystem>().main;
        psMainRight.startRotation = - Mathf.Deg2Rad * slopeDegrees;

        Destroy(landingSpotLeft, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
        Destroy(landingSpotRight, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createFastFallParticle()
    {
        //Reversal of wall cling animations?
        //Creates two fast falling particle animations left and right of the collision spot
        //This gives a visual effect of a change of fall in the air
        Vector2 fastFallPosLeft = new Vector2(player.transform.position.x - landingOffsetX, player.transform.position.y + fastFallOffset);
        Vector2 fastFallPosRight = new Vector2(player.transform.position.x + landingOffsetX, player.transform.position.y + fastFallOffset);
        GameObject fastFallSpotLeft = Instantiate(wallClingPrefab, fastFallPosLeft, Quaternion.Euler(0, 0, 0));
        fastFallSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 1, 0);
        GameObject fastFallSpotRight = Instantiate(wallClingPrefab, fastFallPosRight, Quaternion.Euler(0, 0, 0));
        fastFallSpotRight.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0, 1, 0);
        Destroy(fastFallSpotLeft, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
        Destroy(fastFallSpotRight, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createJumpingParticle(float slopeDegrees)
    {
        //Creates a jumping particle animation opposite the faced direction
        //This gives a visual effect of a jump effect behind the player
        Vector2 jumpingPosLeft = Quaternion.Euler(0, 0, slopeDegrees) * 
            (new Vector3(player.transform.position.x - jumpOffsetX, player.transform.position.y - jumpOffsetY, 0) - player.transform.position) + player.transform.position; //effect on left;
        Vector2 jumpingPosRight = Quaternion.Euler(0, 0, slopeDegrees) * 
            (new Vector3(player.transform.position.x + jumpOffsetX, player.transform.position.y - jumpOffsetY, 0) - player.transform.position) + player.transform.position; //effect on right

        GameObject jumpingSpotLeft = Instantiate(jumpPrefab, jumpingPosLeft, Quaternion.Euler(0, 0, 0));
        jumpingSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        ParticleSystem.MainModule psMainLeft = jumpingSpotLeft.GetComponent<ParticleSystem>().main;
        psMainLeft.startRotation = -Mathf.Deg2Rad * slopeDegrees;

        GameObject jumpingSpotRight = Instantiate(jumpPrefab, jumpingPosRight, Quaternion.Euler(0, 0, 0));
        ParticleSystem.MainModule psMainRight = jumpingSpotRight.GetComponent<ParticleSystem>().main;
        psMainRight.startRotation = -Mathf.Deg2Rad * slopeDegrees;

        Destroy(jumpingSpotLeft, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
        Destroy(jumpingSpotRight, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createWallClingParticle()
    {
        //Creates wall particle animations opposite the faced direction
        //This gives a visual effect of wall interaction behind the player
        tWallClingAnim += Time.deltaTime;
        if (tWallClingAnim > wallClingLoopTime)
        {
            tWallClingAnim = 0;
        }
        else return;
        Vector2 wallClingPos;
        if(this.transform.localScale.x > 0.0f )
            wallClingPos = new Vector2(player.transform.position.x + wallClingOffsetX, player.transform.position.y + wallClingOffsetY); //effect on right
        else wallClingPos = new Vector2(player.transform.position.x - wallClingOffsetX, player.transform.position.y + wallClingOffsetY); //effect on left
        GameObject wallClingSpot = Instantiate(wallClingPrefab, wallClingPos, Quaternion.Euler(0, 0, 0));

        Destroy(wallClingSpot, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createWallJumpParticle()
    {
        //Creates a wall jumping particle animation opposite the faced direction
        //This gives a visual effect of a wall jump effect below the player
        Vector2 wallJumpingPos = new Vector2(player.transform.position.x, player.transform.position.y - wallJumpOffset); //effect below
        GameObject wallJumpingSpot = Instantiate(wallJumpPrefab, wallJumpingPos, Quaternion.Euler(0, 0, 0));
        if (this.transform.localScale.x > 0.0f)
        {
            wallJumpingSpot.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }

        Destroy(wallJumpingSpot, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createDashStartParticle()
    {
        //Creates a dash starting particle animation opposite the faced direction
        //This gives a visual effect of a dash starting behind the player
        Vector2 dashStartPos;
        if (this.transform.localScale.x < 0.0f)
        {
            dashStartPos = new Vector2(player.transform.position.x - dashStartOffsetX, player.transform.position.y + dashStartOffsetY); //effect to the left
        }
        else dashStartPos = new Vector2(player.transform.position.x + dashStartOffsetX, player.transform.position.y + dashStartOffsetY); //effect to the right
        GameObject dashStartSpot = Instantiate(dashStartPrefab, dashStartPos, Quaternion.Euler(0, 0, 0));
        if (this.transform.localScale.x < 0.0f)
        {
            dashStartSpot.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }

        Destroy(dashStartSpot, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createDashContinueParticle(float slopeDegrees)
    {
        //Creates wall particle animations opposite the faced direction
        //This gives a visual effect of wall interaction behind the player
        tDashContAnim += Time.deltaTime;
        if (tDashContAnim > dashContLoopTime)
        {
            tDashContAnim = 0;
        }
        else return;
        Vector2 dashContPos = Quaternion.Euler(0, 0, slopeDegrees) * 
            (new Vector3(player.transform.position.x, player.transform.position.y - dashContinueOffsetY, 0) - player.transform.position) + player.transform.position;//effect on player
        GameObject dashContSpot = Instantiate(dashContinuePrefab, dashContPos, Quaternion.Euler(0, 0, 0));
        ParticleSystem.MainModule psMain = dashContSpot.GetComponent<ParticleSystem>().main;
        psMain.startRotation = -Mathf.Deg2Rad * slopeDegrees;

        Destroy(dashContSpot, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }



    //controller for managing animator FSM settings
    public void Walk(float vel)
    {
        anim.SetBool("isWalking", vel != 0);
    }

    public void Fall(bool fall)
    {
        anim.SetBool("isInAir", fall);
        if(!fall)
            anim.SetBool("hasJumped", false);
    }

    public void Jump(bool jump)
    {
        anim.SetBool("hasJumped", jump);
    }

    public void Wall(bool wall)
    {
        anim.SetBool("isWalling", wall);
    }

    public void Dash(bool dash)
    {
        anim.SetBool("isDashing", dash);
    }

    public void Damage(bool damaged)
    {
        anim.SetBool("isDamageLocked", damaged);
    }

    public void handleMirroring(float vel)
    {
        if (vel < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (vel > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void handleMirroring(bool left)
    {
        if(left)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        } 
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
}

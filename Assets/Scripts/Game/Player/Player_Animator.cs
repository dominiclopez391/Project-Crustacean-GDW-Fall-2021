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
    private const float landingOffset = 0.2f, fastFallOffset = -0.5f, jumpOffset = 0.26f, wallClingOffset = 0.2f, wallJumpOffset = 0.2f, dashStartOffset = 0.2f;
    private float tWallClingAnim = 0f, wallClingLoopTime = 0.2f; //time for repeating the wall clinging animation
    private float tDashContAnim = 0f, dashContLoopTime = 0.02f; // time for repeating the dash continuing animation


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

    public void createLandingParticle()
    {
        //Creates two landing particle animations left and right of the collision spot
        //This gives a visual effect of a fall impact with the ground
        Vector2 landingPosLeft = new Vector2(player.transform.position.x - landingOffset, player.transform.position.y);
        Vector2 landingPosRight = new Vector2(player.transform.position.x + landingOffset, player.transform.position.y);
        GameObject landingSpotLeft = Instantiate(landPrefab, landingPosLeft, Quaternion.Euler(0, 0, 0));
        landingSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        GameObject landingSpotRight = Instantiate(landPrefab, landingPosRight, Quaternion.Euler(0, 0, 0));
        Destroy(landingSpotLeft, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
        Destroy(landingSpotRight, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createFastFallParticle()
    {
        //Reversal of wall cling animations?
        //Creates two fast falling particle animations left and right of the collision spot
        //This gives a visual effect of a change of fall in the air
        Vector2 fastFallPosLeft = new Vector2(player.transform.position.x - landingOffset, player.transform.position.y + fastFallOffset);
        Vector2 fastFallPosRight = new Vector2(player.transform.position.x + landingOffset, player.transform.position.y + fastFallOffset);
        GameObject fastFallSpotLeft = Instantiate(wallClingPrefab, fastFallPosLeft, Quaternion.Euler(0, 0, 0));
        fastFallSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 1, 0);
        GameObject fastFallSpotRight = Instantiate(wallClingPrefab, fastFallPosRight, Quaternion.Euler(0, 0, 0));
        fastFallSpotRight.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0, 1, 0);
        Destroy(fastFallSpotLeft, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
        Destroy(fastFallSpotRight, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createJumpingParticle()
    {
        //Creates a jumping particle animation opposite the faced direction
        //This gives a visual effect of a jump effect behind the player
        Vector2 jumpingPosLeft = new Vector2(player.transform.position.x - jumpOffset, player.transform.position.y + jumpOffset); //effect on left;
        Vector2 jumpingPosRight = new Vector2(player.transform.position.x + jumpOffset, player.transform.position.y + jumpOffset); //effect on right
        GameObject jumpingSpotLeft = Instantiate(jumpPrefab, jumpingPosLeft, Quaternion.Euler(0, 0, 0));
        jumpingSpotLeft.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        GameObject jumpingSpotRight = Instantiate(jumpPrefab, jumpingPosRight, Quaternion.Euler(0, 0, 0));
        
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
            wallClingPos = new Vector2(player.transform.position.x + wallClingOffset, player.transform.position.y); //effect on right
        else wallClingPos = new Vector2(player.transform.position.x - wallClingOffset, player.transform.position.y); //effect on left
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
            dashStartPos = new Vector2(player.transform.position.x - dashStartOffset, player.transform.position.y); //effect to the left
        }
        else dashStartPos = new Vector2(player.transform.position.x + dashStartOffset, player.transform.position.y); //effect to the right
        GameObject dashStartSpot = Instantiate(dashStartPrefab, dashStartPos, Quaternion.Euler(0, 0, 0));
        if (this.transform.localScale.x < 0.0f)
        {
            dashStartSpot.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
        }

        Destroy(dashStartSpot, PARTICLE_SYSTEM_DEPSPAWN_TIMER); // cleans up spriteless object after animation
    }

    public void createDashContinueParticle()
    {
        //Creates wall particle animations opposite the faced direction
        //This gives a visual effect of wall interaction behind the player
        tDashContAnim += Time.deltaTime;
        if (tDashContAnim > dashContLoopTime)
        {
            tDashContAnim = 0;
        }
        else return;
        Vector2 dashContPos = new Vector2(player.transform.position.x, player.transform.position.y); //effect on player
        GameObject dashContSpot = Instantiate(dashContinuePrefab, dashContPos, Quaternion.Euler(0, 0, 0));

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

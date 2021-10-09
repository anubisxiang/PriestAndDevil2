using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float moveSpeed=150f;
    int moveSign=0;
    Vector3 endPos,midPos;

    void Update(){  
        if(moveSign==1){
            transform.position=Vector3.MoveTowards(transform.position,midPos,moveSpeed*Time.deltaTime);
            if(transform.position==midPos){
                moveSign=2;
            }
        }else if(moveSign==2){
            transform.position=Vector3.MoveTowards(transform.position,endPos,moveSpeed*Time.deltaTime);
            if(transform.position==endPos){
                moveSign=0;
            }
        }
    }

    public void MovePosition(Vector3 position){
        endPos=position;
        if(position.y == transform.position.y){
            moveSign=2;
            return;
        }else if(position.y<transform.position.y){
            midPos=new Vector3(position.x,transform.position.y,position.z);
        }else{
            midPos=new Vector3(transform.position.x,position.y,position.z);
        }
        moveSign=1;
    }
}

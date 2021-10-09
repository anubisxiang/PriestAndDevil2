using UnityEngine;
using ControllerApplication;
using InterfaceApplication;
   
public class Click : MonoBehaviour
{
    IUserAction action;
    RoleModel role=null;
    BoatModel boat=null;
    public void setRole(RoleModel role){
        this.role=role;
    }
    public void setBoat(BoatModel boat){
        this.boat=boat;
    }

    void Start(){
        action=SSDirect.getInstance().CurrentSceneController as IUserAction;
    }
    void OnMouseDown(){
        if(boat==null && role==null) return;

        if(boat!=null) action.moveBoat();
        else if(role!=null) action.moveRole(role);
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;
using ControllerApplication;
using InterfaceApplication;

public class FirstController : MonoBehaviour,ISceneController,IUserAction{
    public LandModel startLand;
    public LandModel endLand;
    public Water water;
    public BoatModel boat;
    public RoleModel[] roles;
    public UserGUI GUI;

    public void LoadResoureces(){    
        water=new Water();//构建水
        startLand=new LandModel("start");//构建两岸
        endLand=new LandModel("end");
        boat=new BoatModel();//构建船
        roles=new RoleModel[6];//构建角色
        for(int i=0;i<3;i++) {
            RoleModel role=new RoleModel("priest", startLand.getEmptyPos());
            role.setName("priest"+i);
            startLand.addRole(role);
            roles[i]=role;
        }
        for(int i=0;i<3;i++) {
            RoleModel role=new RoleModel("devil", startLand.getEmptyPos());
            role.setName("devil"+i);
            startLand.addRole(role);
            roles[i+3]=role;
        }
    }
    public void moveBoat(){
        if (boat.empty() || GUI.sign!=0) return;
        boat.boatMove();
        GUI.sign=check();
    }
    public void moveRole(RoleModel role){
        if(GUI.sign!=0) return;
        if(role.getOnBoat()==1) {
            boat.deleteRole(role.getName());
            role.setOnBoat(0);
            role.getRole().transform.parent=null;
            if(boat.getBoatSign()==1) {
                role.roleMove(startLand.getEmptyPos());
                role.setLandSign(1);
                startLand.addRole(role);
            }
            else {
                role.roleMove(endLand.getEmptyPos());
                role.setLandSign(-1);
                endLand.addRole(role);
            }       
        }else{
            if(role.getLandSign()==1) {
                if(boat.getEmptyNumber()==-1 || role.getLandSign()!=boat.getBoatSign()) return;
                startLand.deleteRole(role.getName());
                role.roleMove(boat.getEmptyPos());
                role.getRole().transform.parent=boat.getBoat().transform;
                role.setOnBoat(1);
                boat.addRole(role);
            }else{
                if(boat.getEmptyNumber() == -1 || role.getLandSign()!=boat.getBoatSign()) return;
                endLand.deleteRole(role.getName());
                role.roleMove(boat.getEmptyPos());
                role.getRole().transform.parent = boat.getBoat().transform;
                role.setOnBoat(1);
                boat.addRole(role);
            }
        }
        GUI.sign=check();
    }
    public void reStart(){
        SceneManager.LoadScene(0);
    }
    public int check(){
        //0-游戏1继续；1-游戏成功；-1-游戏失败
        int[] boatRole=boat.getRoleNum();
        int[] startRole=startLand.getRoleNum();
        int[] endRole=endLand.getRoleNum();

        if(endRole[0]+endRole[1]==6) return 1;

        if(boat.getBoatSign()==1){
            startRole[0]+=boatRole[0];
            startRole[1]+=boatRole[1];
        }else{
            endRole[0]+=boatRole[0];
            endRole[1]+=boatRole[1];
        }

        if((endRole[0]>0 && endRole[1]>endRole[0]) || (startRole[0]>0 && startRole[1]>startRole[0]))
            return -1;
        return 0;
    }

    void Start(){
        SSDirect director=SSDirect.getInstance();
        director.CurrentSceneController=this;
        GUI=gameObject.AddComponent<UserGUI>() as UserGUI;
        LoadResoureces();
    }
}

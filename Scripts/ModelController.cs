using UnityEngine;

namespace ControllerApplication{
    public class Water{
        GameObject water;
        public Water(){
            water=Object.Instantiate(Resources.Load<GameObject>("Prefabs/Water"),  
                Vector3.zero, Quaternion.identity) as GameObject;
            water.name="water";
        }
    }

    public class LandModel{
        GameObject land;
        int landSign;
        Vector3[] positions;
        RoleModel[] roles = new RoleModel[6];

        public LandModel(string str){
            positions=new Vector3[] {
                new Vector3(6.35F,2F,0), new Vector3(7.35f,2F,0), new Vector3(8.35f,2F,0),
                new Vector3(9.35f,2F,0), new Vector3(10.35f,2F,0), new Vector3(11.35f,2F,0)
            };
            if (str=="start") landSign=1;
            else landSign=-1;
            land=Object.Instantiate(Resources.Load<GameObject>("Prefabs/Land"),
                    new Vector3(9*landSign,1,0), Quaternion.identity) as GameObject;
            land.name =str+"Land";
        }

        //一系列用于处理角色上岸和上船的成员函数
        public int getLandSign(){
            return landSign;
        }
        public int getEmptyNumber(){
            for(int i=0;i<6;i++){
                if(roles[i]==null)
                    return i;
            }
            return -1;
        }
        public Vector3 getEmptyPos(){
            Vector3 res=positions[getEmptyNumber()];
            res.x=res.x*landSign;
            return res;
        }
        public int[] getRoleNum(){
            int[] res={0,0};
            for(int i=0;i<6;i++){
                if(roles[i]!=null){
                    if(roles[i].getSign()==0) res[0]++;
                    else res[1]++;
                }
            }
            return res;
        }

        public void addRole(RoleModel role){
            roles[getEmptyNumber()]=role;
        }
        public RoleModel deleteRole(string name){
            for(int i=0;i<6;i++){
                if(roles[i]!=null && roles[i].getName()==name){
                    RoleModel res=roles[i];
                    roles[i]=null;
                    return res;
                }
            }
            return null;
        }
    }

    public class BoatModel{
        GameObject boat;
        int boatSign;
        Vector3[] startPos,endPos;
        RoleModel[] roles=new RoleModel[2];
        Move move;
        Click click;

        public BoatModel(){
            boat=Object.Instantiate(Resources.Load<GameObject>("Prefabs/Boat"), 
                new Vector3(5,0.5f,0), Quaternion.identity) as GameObject;
            boatSign=1;
            startPos=new Vector3[]{ new Vector3(5.5f,1,0), new Vector3(4.5f,1,0) };
            endPos = new Vector3[]{ new Vector3(-4.5f,1,0), new Vector3(-5.5f,1,0) };
            move=boat.AddComponent(typeof(Move)) as Move;
            click=boat.AddComponent(typeof(Click)) as Click;
            click.setBoat(this);
            boat.name="boat";
        }

        //一系列用于处理角色上岸和上船以及船移动的成员函数
        public int getBoatSign(){
            return boatSign;
        }
        public int getEmptyNumber(){
            for(int i=0;i<2;i++){
                if(roles[i]==null)
                    return i;
            }
            return -1;
        }
        public Vector3 getEmptyPos(){
            Vector3 res;
            if(boatSign==1) res=startPos[getEmptyNumber()];
            else res=endPos[getEmptyNumber()];
            return res;
        }
        public int[] getRoleNum(){
            int[] res={0,0};
            for(int i=0;i<2;i++){
                if(roles[i]!=null){
                    if(roles[i].getSign()==0) res[0]++;
                    else res[1]++;
                }
            }
            return res;
        }

        public void addRole(RoleModel role){
            roles[getEmptyNumber()]=role;
        }
        public RoleModel deleteRole(string name){
            for(int i=0;i<2;i++){
                if(roles[i]!=null && roles[i].getName()==name){
                    RoleModel res=roles[i];
                    roles[i]=null;
                    return res;
                }
            }
            return null;
        }

        public void boatMove(){
            if(boatSign==-1) move.MovePosition(new Vector3(5,0.5f,0));
            else move.MovePosition(new Vector3(-5,0.5f,0));
            boatSign*=-1;
        }

        public GameObject getBoat(){
            return boat;
        }

        public bool empty(){
            for(int i=0;i<2;i++){
                if(roles[i]!=null) return false;
            }
            return true;
        }
    }

    public class RoleModel{
        GameObject role;
        int roleSign;
        int onBoat;
        int landSign;
        Move move;
        Click click;

        public RoleModel(string name,Vector3 pos){
            if(name=="priest") {
                role=Object.Instantiate(Resources.Load<GameObject>("Prefabs/Priest"),
                    pos, Quaternion.Euler(0,-90,0)) as GameObject;
                roleSign=0;
            }else{
                role=Object.Instantiate(Resources.Load<GameObject>("Prefabs/Devil"),
                    pos, Quaternion.Euler(0,-90,0)) as GameObject;
                roleSign=1;
            }
            landSign=1;
            move=role.AddComponent(typeof(Move)) as Move;
            click=role.AddComponent(typeof(Click)) as Click;
            click.setRole(this);
        }

        public string getName(){ 
            return role.name; 
        }
        public int getLandSign(){ 
            return landSign; 
        }
        public void setLandSign(int land){ 
            landSign=land; 
        }
        public int getSign(){
            return roleSign;
        }
        public int getOnBoat(){ 
            return onBoat; 
        }
        public void setOnBoat(int a){ 
            onBoat=a; 
        }
        public void setName(string name){ 
            role.name=name; 
        }
        public void setPosition(Vector3 pos) {
            role.transform.position = pos;
        }
        public GameObject getRole(){
            return role;
        }
        public void roleMove(Vector3 pos){
            move.MovePosition(pos);
        }
    }
}
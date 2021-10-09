using UnityEngine;
using ControllerApplication;

namespace  InterfaceApplication{
    //场景接口   
    public interface ISceneController{
        void LoadResoureces();
    }

    //用户动作接口
    public interface IUserAction{
        void moveBoat();
        void moveRole(RoleModel role);
        void reStart();
        int check();
    }
}
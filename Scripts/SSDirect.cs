using UnityEngine;
using ControllerApplication;
using InterfaceApplication;

public class SSDirect : System.Object{
    private static SSDirect instance;
    public ISceneController CurrentSceneController{ get; set;}   

    public static SSDirect getInstance(){
        if(instance==null) instance=new SSDirect();
        return instance;
    }
}

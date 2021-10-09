using UnityEngine;
using InterfaceApplication;

public class UserGUI : MonoBehaviour
{
    public IUserAction action;
    public int sign=0;
    void Start() {
        action = SSDirect.getInstance().CurrentSceneController as IUserAction;    
    }
    void OnGUI() {
        GUIStyle text_style;
        GUIStyle button_style;
        text_style = new GUIStyle() {
            fontSize = 30
        };
        button_style = new GUIStyle("button") {
            fontSize = 15
        };

        if(sign==-1) {
            GUI.Label(new Rect(Screen.width/2-90,Screen.height/2-120,100,50),"Gameover!",text_style);  
        }
        else if(sign==1) {
            GUI.Label(new Rect(Screen.width/2-80,Screen.height/2-120,100,50),"You Win!",text_style);
        }

        if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-200,100,50),"Restart",button_style)) {
            action.reStart();
            sign = 0;
        }
    }
}

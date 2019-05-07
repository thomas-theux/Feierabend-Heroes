using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class VibrationManager : MonoBehaviour {

    private Player player { get { return ReInput.players.GetPlayer(0); } }


    private void Update() {
        var ds4 = GetFirstDS4(player);

        if (player.GetButtonDown("X")) {
            ds4.SetVibration(0, 1f, 1f);
        }
    }


    private IDualShock4Extension GetFirstDS4(Player player) {
        foreach(Joystick j in player.controllers.Joysticks) {
            // Use the interface because it works for both PS4 and desktop platforms
            IDualShock4Extension ds4 = j.GetExtension<IDualShock4Extension>();
            if(ds4 == null) continue;
            return ds4;
        }
        return null;
    }

}
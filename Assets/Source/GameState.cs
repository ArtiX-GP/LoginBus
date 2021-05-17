using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameStateListener {
    void onGameStateUpdated();
}

public class GameState
{

    private static readonly Dictionary<string, GameStateListener> LISTENERS = new Dictionary<string, GameStateListener>();

    private static readonly Dictionary<string, dynamic> GAME_DATA = new Dictionary<string, dynamic>();

    public static void AddListener(string key, GameStateListener l) {
        if (l != null && key != null && key.Trim() != "") {
            if (!LISTENERS.ContainsKey(key)) {
                LISTENERS.Add(key, l);
            }
        }
    }

    public static void RemoveListener(string key) {
        if (key != null && key.Trim() != "") {
            LISTENERS.Remove(key);
        }
    }

    public static string GetString(string key) {
        return GAME_DATA[key] as string;
    }

    public static int GetInt(string key, int defVal = 0) {
        if (GAME_DATA.ContainsKey(key)) {
            return ((int)GAME_DATA[key]);
        }
        return defVal;
    }

    public static void SetValue(string key, dynamic value) {
        if (GAME_DATA.ContainsKey(key)) {
            GAME_DATA[key] = value;
        } else {
            GAME_DATA.Add(key, value);
        }

        foreach (GameStateListener l in LISTENERS.Values) {
            l.onGameStateUpdated();
        }
    }


}




using UnityEngine;

public static class ColorUtility {

    public static Color32 GetCardColor (string color) {

        switch (color.ToLower().Trim()) {

            case "red":
                return new Color(170, 70, 70, 255);
            case "green":
                return new Color32(90, 140, 80, 255);
            case "blue":
                return new Color32(70, 140, 180, 255);
            case "neutral":
                return new Color32(130, 130, 130, 255);
            case "villain":
                return new Color32(44, 44, 44, 255);
            default:
                return Color.white;
        }
    }
}

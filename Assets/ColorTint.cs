using UnityEngine;
using System.Collections;

public class ColorTint : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        // duplicate the original texture
        var texture = Instantiate(rend.sprite.texture) as Texture2D;
        Debug.Log("Copied texture: " + texture.name);

        // colors used to tint the first 3 mip levels
        Color[] colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        int mipCount = Mathf.Min(3, texture.mipmapCount);
        Debug.Log("Mip count: " + mipCount);

        // tint each mip level
        for (var mip = 0; mip < mipCount; ++mip)
        {
            var cols = texture.GetPixels(mip);
            Debug.Log("Pixel count: " + cols.Length);

            for (var i = 0; i < cols.Length; ++i)
            {
                cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
            }
            texture.SetPixels(cols, mip);
        }
        // actually apply all SetPixels, don't recalculate mip levels
        texture.Apply(false);

        // create a new sprite using the tinted texture and assign it to the renderer
        string spriteName = rend.sprite.name;
        rend.sprite = Sprite.Create(texture, rend.sprite.rect, new Vector2(0.5f, 0.5f));
        rend.sprite.name = spriteName;
    }

    // Update is called once per frame
    void Update()
    { }
}


using UnityEngine;
using System.Collections;

public class CSprite : CGameObject
{
    public UISprite uiSprite;

    private UISpriteAnimation m_spriteAnimation;
    public CSprite()
    {
        base.m_componentType = eComponentType.Sprite;
        
    }
    public override void Init()
    {
        uiSprite = _Go.GetComponent<UISprite>();
    }

    //bNeedPerfect = trueʱ�� sprite�Ĵ�С����ΪͼƬ�Ĵ�С,������Ӧsprite�Ĵ�С
    public void SetSprite(UIAtlas atlas,string name,bool bNeedPerfect = false)
    {
        uiSprite.atlas = atlas;
        uiSprite.spriteName = name;

        if (bNeedPerfect)
        {
            if (uiSprite.type != UISprite.Type.Simple)
            {
                //Common.LogWarning("SetSprite:" + name + "..warning:type is " + m_sprite.type);
                uiSprite.type = UISprite.Type.Simple;
            }
            uiSprite.MakePixelPerfect();
        }
    }

    //public void ResetSprite()
    //{
    //    sprite.atlas = null;
    //    sprite.spriteName = "";

    //}
    //ͬһͼ��ֱ�Ӹ�spriteName����.
    //public void SetSprite(string name, bool bNeedPerfect = false)
    //{
    //    m_sprite.spriteName = name;

    //    if (bNeedPerfect)
    //    {
    //        m_sprite.MakePixelPerfect();
    //    }
    //}
    //public void SetSpriteType(UISprite.Type type)
    //{
    //    sprite.type = type;
    //}

    public void SetSpriteColor(Color color)
    {
        uiSprite.color = color;
    }

    //public Vector2 GetSpriteSize()
    //{
    //    Vector3 scale = sprite.transform.localScale;
    //    return new Vector2(scale.x, scale.y);
    //}

    //public string GetSpriteName()
    //{
    //    return sprite.spriteName;
    //}
    /// <summary>
    /// ���þ��鶯������ false ��ͣ��true����
    /// </summary>
    public void PlayAnimation(bool playing)
    {
        m_spriteAnimation = _Go.GetComponent<UISpriteAnimation>();
        if (m_spriteAnimation == null)
        {
            Debug.LogError("the sprite could't find UISpriteAnimation component !");
            return;
        }
        m_spriteAnimation.enabled = playing;
    }

    /// <summary>
    /// ���þ��鶯������ play = false ��ͣ��true���� ��namePrefix��Ҫ���ŵľ�������ͼ ��framer �������� ��loop =false ��ѭ�� trueѭ�� ��Ĭ�ϲ�ѭ��
    /// </summary>
    public void SetSpriteAnimation(bool play, string namePrefix, int framerFPS = 0, bool loop = false)
    {
        m_spriteAnimation = _Go.GetComponent<UISpriteAnimation>();
        if (m_spriteAnimation == null)
        {
            Debug.LogError("the sprite could't find UISpriteAnimation component !");
            return;
        }
        if (framerFPS >= 60)
        {
            framerFPS = 60;
        }
        m_spriteAnimation.namePrefix = namePrefix;
        m_spriteAnimation.framesPerSecond = framerFPS;
        m_spriteAnimation.loop = loop;
        m_spriteAnimation.enabled = play;
    }

    public bool IsComplete()
    {
        m_spriteAnimation = _Go.GetComponent<UISpriteAnimation>();
        if (m_spriteAnimation == null)
        {
            Debug.LogError("the sprite could't find UISpriteAnimation component !");
            return false;
        }
        if(m_spriteAnimation.loop)
        {
            return false;
        }

        return m_spriteAnimation.isPlaying;
    }
}

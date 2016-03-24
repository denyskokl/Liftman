using System;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public abstract class BaseSpriteAnimation : MonoBehaviour
{

    public bool isPlaying
    {
        get { return m_playing; }
    }

    public bool Loop
    {
        get
        {
            return _loop;
        }

        set
        {
            _loop = value;
        }
    }

    [SerializeField]
    private Sprite[] m_frames;                // User defined frames

    [SerializeField]
    private float m_fps;                    // User defined framerate

    [SerializeField]
    private bool m_playOnAwake;            // Should we play animation automatically on awake?

    private float m_frameTime;            // Cached target frame time

    private int m_currentFrame;        // Current frame index
    private float m_timer;              // Current frame time

    private bool m_playing = false;        // Is currently playing?
    [SerializeField]
    private bool _loop = false;
    private Action _onFinish;

    public bool destroyOnCompleted;

    protected abstract void SetSprite(Sprite sprite);
    protected abstract void Init();
    public void Play(bool loop, Action onFinish = null)
    {
        _onFinish = onFinish;
        Loop = loop;
        if (m_fps <= 0)
        {

            return;
        }

        m_frameTime = 1.0f / m_fps;            // DOH! time is in seconds, not milis :)

        m_playing = true;
        SetFrame(0);
    }

    void Update()
    {
        UpdateFrame(Time.deltaTime);
    }
    public void UpdateFrame(float deltaTime)
    {
        if (m_playing)
        {
            m_timer += deltaTime;

            if (m_timer > m_frameTime)
            {
                GetNextFrame();
            }
        }
    }
    void Awake()
    {
        Init();
        if (m_playOnAwake)
        {
            Play(Loop);
        }
    }

    private void GetNextFrame()
    {
        m_currentFrame++;

        if (m_currentFrame < m_frames.Length)
        {
            SetFrame(m_currentFrame);
            return;
        }
        if (Loop)
        {
            m_currentFrame = 0;
            SetFrame(m_currentFrame);
            return;
        }


        if (destroyOnCompleted)
        {
            Destroy(gameObject);
        }

        if (_onFinish != null)
        {
            _onFinish();
        }
        m_playing = false;
    }

    private void SetFrame(int frame)
    {
        m_currentFrame = frame;
        m_timer = 0;

        SetSprite(m_frames[frame]);
    }

    public void SetIsPlay()
    {
        isPlay = !isPlay;
    }
    private bool isPlay = false;

}
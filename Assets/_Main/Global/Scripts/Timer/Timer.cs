using System;
using System.Collections;
using UnityEngine;

public class Timer : IDisposable
{
    private Coroutine _timeRoutine;
    private Action _listener;
    private float _interval;
    private float _duration;
    
    private bool _isFrameInterval;
    private bool _durationSet;
    private bool _onPause;
    
    public Timer(Action listener, float interval = 0f, float duration = 0f)
    {
        _listener = listener;
        if (duration > 0f) _durationSet = true;
        if (interval > 0f) _isFrameInterval = true;
        
        _interval = interval;
        _duration = duration;
    }

    public void Start() { _timeRoutine = G.Coroutine.Start(Time()); }

    private IEnumerator Time()
    {
        var duration = _duration;
        var time = 0f;
        var waitUntilResume = new WaitUntil(() => !_onPause);
        while (true)
        {
            yield return null;
            if (_onPause) { yield return waitUntilResume; }
            if (_isFrameInterval) { _listener?.Invoke(); continue; }
            
            var delta = UnityEngine.Time.deltaTime;
            time += delta;
            if (time >= _interval)
            { _listener?.Invoke(); time = 0f; }

            if (!_durationSet) continue;
            duration -= delta; if (duration <= 0f) { Stop(); }
        }
    }

    public void Stop() { if (_timeRoutine != null) G.Coroutine.Stop(_timeRoutine); }

    public void Pause() => _onPause = true;
    public void Resume() => _onPause = false;
    
    public void Dispose()
    {
        if (_timeRoutine != null) G.Coroutine.Stop(_timeRoutine);
        _timeRoutine = null;
        _listener = null;
    }
}
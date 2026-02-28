namespace AntDesign.WPF.Demo.ViewModels;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> _argsCache = new();

    private bool _disposed;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        var handler = PropertyChanged;
        if (handler == null) return;

        var args = _argsCache.GetOrAdd(propertyName ?? string.Empty, static name => new PropertyChangedEventArgs(name));
        handler(this, args);
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PacketBuffer : IDisposable {

    List<byte> _bufferList;
    byte[] _readBuffer;
    int _readPos;
    bool _buffUpdate = false;

    #region Public Commands
    public PacketBuffer()
    {
        _bufferList = new List<byte>();
        _readPos = 0;
    }

    public int GetReadPos()
    {
        return _readPos;
    }

    public byte[] ToArray()
    {
        return _bufferList.ToArray();
    }

    public int Count()
    {
        return _bufferList.Count;
    }

    public int Length()
    {
        return Count() - _readPos;
    }

    public void Clear()
    {
        _bufferList.Clear();
        _readPos = 0;
    }
    #endregion

    #region Write Data
    public void WriteByte(byte input)
    {
        _bufferList.Add(input);
        _buffUpdate = true;
    }

    public void WriteBytes(byte[] input)
    {
        _bufferList.AddRange(input);
        _buffUpdate = true;
    }

    public void WriteInteger(int input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(input));
        _buffUpdate = true;
    }

    public void WriteFloat(float input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(input));
        _buffUpdate = true;
    }

    public void WriteString(string input)
    {
        _bufferList.AddRange(BitConverter.GetBytes(Encoding.UTF8.GetByteCount(input)));
        _bufferList.AddRange(Encoding.UTF8.GetBytes(input));
        _buffUpdate = true;
    }

    public void WriteVector3(float[] pos)
    {
        _bufferList.AddRange(BitConverter.GetBytes(pos[0]));
        _bufferList.AddRange(BitConverter.GetBytes(pos[1]));
        _bufferList.AddRange(BitConverter.GetBytes(pos[2]));
        _buffUpdate = true;
    }

    public void WriteQuaternion(float[] rot)
    {
        _bufferList.AddRange(BitConverter.GetBytes(rot[0]));
        _bufferList.AddRange(BitConverter.GetBytes(rot[1]));
        _bufferList.AddRange(BitConverter.GetBytes(rot[2]));
        _bufferList.AddRange(BitConverter.GetBytes(rot[3]));
        _buffUpdate = true;
    }
    #endregion

    #region Read Data
    public byte ReadByte(bool peek = true)
    {
        if (_bufferList.Count > _readPos)
        {
            if (_buffUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _buffUpdate = false;
            }

            byte value = _readBuffer[_readPos];
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 1;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer past it's limit!");
        }
    }

    public byte[] ReadBytes(int Length, bool peek = true)
    {
        if (_buffUpdate)
        {
            _readBuffer = _bufferList.ToArray();
            _buffUpdate = false;
        }

        byte[] value = _bufferList.GetRange(_readPos, Length).ToArray();
        if (peek & _bufferList.Count > _readPos)
        {
            _readPos += Length;
        }
        return value;
    }

    public int ReadInteger(bool peek = true)
    {
        if(_bufferList.Count > _readPos)
        {
            if(_buffUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _buffUpdate = false;
            }

            int value = BitConverter.ToInt32(_readBuffer, _readPos);
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer past it's limit!");
        }
    }

    public float ReadFloat(bool peek = true)
    {
        if (_bufferList.Count > _readPos)
        {
            if (_buffUpdate)
            {
                _readBuffer = _bufferList.ToArray();
                _buffUpdate = false;
            }

            float value = BitConverter.ToSingle(_readBuffer, _readPos);
            if (peek & _bufferList.Count > _readPos)
            {
                _readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("Buffer past it's limit!");
        }
    }

    public string ReadString(bool peek = true)
    {
        int length = ReadInteger(true);
        if (_buffUpdate)
        {
            _readBuffer = _bufferList.ToArray();
            _buffUpdate = false;
        }

        string value = Encoding.UTF8.GetString(_readBuffer, _readPos, length);
        if (peek & _bufferList.Count > _readPos)
        {
            _readPos += length;
        }
        return value;
    }

    public float[] ReadVector3()
    {
        return new float[] { ReadFloat(), ReadFloat(), ReadFloat() };
    }

    public float[] ReadQuternion()
    {
        return new float[] { ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat() };
    }
    #endregion

    #region IDispose
    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                _bufferList.Clear();
            }
            _readPos = 0;
        }
        disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}

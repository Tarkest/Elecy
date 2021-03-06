﻿public class SynergyUpdate : BaseUpdate<int>
{
    public override void Callback()
    {
        lock (locker)
        {
            if (currentValue > 0)
            {
                UpdateContainer<int> value;
                if (updateLibrary.TryGetValue(currentIndex, out value))
                {
                    if (!value.sent)
                    {
                        SendDataUDP.SendSNUpdate(mObject.type, mObject.index, currentIndex, currentValue);
                    }
                }
            }
            else
            {
                // Send Death
            }
        }
    }

    public void ChangeSN(int value, bool Heal = false)
    {
        lock (locker)
        {
            currentIndex++;
            currentValue = Heal ? currentValue + value : currentValue - value;
            updateLibrary.Add(currentIndex, new UpdateContainer<int>(currentValue));
        }
    }

    protected override void SendStepBack()
    {
        SendDataUDP.SendSNStepback(mObject.type, mObject.index, currentIndex);
    }
}


// By Scott Heckel
using Microsoft.SPOT.Hardware;
using MorseCodeLibrary;
using SecretLabs.NETMF.Hardware.Netduino;

namespace MorseCode
{
    public class Program
    {
        private static OutputPort LED;

        public static void Main()
        {
            LED = new OutputPort(Pins.ONBOARD_LED, false);

            (new MorseCodeLibrary.MorseCode())
                .CallOnToggle(new MorseCodeBlinkEventHandler(ToggleLed))
                .SetDotLength(100)
                .Display("hello world ")
                .Display("i come in peace ")
                .Display("i am netduino");
        }

        public static void ToggleLed(object sender, MorseCodeBlinkEventArgs eventArgs)
        {
            LED.Write(eventArgs.IsOn);
        }

    }
}

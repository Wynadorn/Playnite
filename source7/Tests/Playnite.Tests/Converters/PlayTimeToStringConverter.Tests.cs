﻿using System.Globalization;

namespace Playnite.Tests;

[TestFixture]
public class PlayTimeToStringConverterTests
{
    private static string? Convert(object value, object? formatToDaysParameter = null)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return PlayTimeToStringConverter.Instance.Convert(value, typeof(string), formatToDaysParameter, CultureInfo.CurrentCulture) as string;
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Test]
    public void ConvertLocalizationStringsTest()
    {
        ulong? zeroSeconds = 0;
        ulong? totalSecondsInMinute = 60;
        ulong? totalSecondsInHour = totalSecondsInMinute * 60;
        ulong? totalSecondsInDay = totalSecondsInHour * 24;

        // formatToDaysParameter set to null/false tests
        Assert.AreEqual("Not Played", Convert(zeroSeconds, false));
        Assert.AreEqual("59 seconds", Convert(totalSecondsInMinute - 1, false));
        Assert.AreEqual("1 minutes", Convert(totalSecondsInMinute, false));
        Assert.AreEqual("24h 1m", Convert(totalSecondsInDay + totalSecondsInMinute, false));

        // formatToDaysParameter set to true
        Assert.AreEqual("Not Played", Convert(zeroSeconds, true));
        Assert.AreEqual("59 seconds", Convert(totalSecondsInMinute - 1, true));
        Assert.AreEqual("1 minutes", Convert(totalSecondsInMinute, true));
        Assert.AreEqual("23h 59m", Convert(totalSecondsInDay - 1, true));
        Assert.AreEqual("1d 0h 0m", Convert(totalSecondsInDay, true));

        var testSeconds = totalSecondsInDay + (totalSecondsInHour * 2) + (totalSecondsInMinute * 30);
        Assert.AreEqual("1d 2h 30m", Convert(testSeconds, true));
    }
}

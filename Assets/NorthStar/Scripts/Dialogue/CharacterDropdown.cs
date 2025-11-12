// Copyright (c) Meta Platforms, Inc. and affiliates.
using System;
using Meta.XR.Samples;
using UnityEngine;

namespace NorthStar
{
    /// <summary>
    /// Tag for a string to use options from the character register instead of an input field
    /// </summary>
    [MetaCodeSample("NorthStar")]
    [AttributeUsage(AttributeTargets.Field)]
    public class CharacterDropdown : PropertyAttribute
    {
    }
}

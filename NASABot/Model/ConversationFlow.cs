// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

namespace NASABot.Model
{
    public class ConversationFlow
    {
        public Question Question { get; set; }
    }

    public enum Question
    {
        None,
        ObservationStartDateQuestion,
        ObservationEndDateQuestion,
        ObservationTypeQuestion,
    }
}
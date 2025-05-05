﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Exceptions
{
    public class CommentRequiredException : Exception
    {
        public CommentRequiredException(string? message) : base(message)
        {
        }
    }
}

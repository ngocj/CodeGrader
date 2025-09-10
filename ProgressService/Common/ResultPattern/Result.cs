﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResultPattern
{
    public class Result
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public ErrorDetail? ErrorDetail { get; set; }

        public Result()
        {
        }

        protected Result(string message, bool isSuccess, ErrorDetail? errorDetail)
        {
            Message = message;
            IsSuccess = isSuccess;
            ErrorDetail = errorDetail;
        }

        public static Result Success(string message)
        {
            return new Result(message, true, null);

        }

        public static Result Success()
        {
            return new Result(string.Empty, true, null);

        }

        public static Result Failure(string message, ErrorDetail errorDetail)
        {
            return new Result(message, false, errorDetail);
        }

        public static Result Failure(string message)
        {
            return new Result(message, false, null);
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result()
        {
        }
        protected Result(string message, bool isSuccess, ErrorDetail? errorDetail, T? data) : base(message, isSuccess, errorDetail)
        {
            Data = data;
        }

        public static Result<T> Success(string message, T? data)
        {
            return new Result<T>(message, true, null, data);
        }

        public static Result<T> Success(T? data)
        {
            return new Result<T>(string.Empty, true, null, data);
        }

        public static new Result<T> Failure(string message, ErrorDetail errorDetail)
        {
            return new Result<T>(message, false, errorDetail, default);
        }

        public static new Result<T> Failure(string message)
        {
            return new Result<T>(message, false, null, default);
        }
    }
}

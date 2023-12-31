using System;

namespace Superscrot
{
    /// <summary>
    /// Represents errors that occur when a connection could not be made to the server.
    /// </summary>
    [Serializable]
    public class ConnectionFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class.
        /// </summary>
        public ConnectionFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class with the
        /// specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConnectionFailedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class with the
        /// specified error message and hostname.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="hostname">
        /// The hostname of the server that could not be connected to.
        /// </param>
        public ConnectionFailedException(string message, string hostname)
            : base(message)
        {
            Hostname = hostname;
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class with the
        /// specified error message and a reference to the inner exception that
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.
        /// </param>
        public ConnectionFailedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class with the
        /// specified error message, the hostname of the server and a reference
        /// to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="hostname">
        /// The hostname of the server that could not be connected to.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.
        /// </param>
        public ConnectionFailedException(string message, string hostname, Exception inner)
            : base(message, inner)
        {
            Hostname = hostname;
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.ConnectionFailedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains
        /// contextual information about the source or destination.
        /// </param>
        protected ConnectionFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            Hostname = info.GetString("Hostname");
        }

        /// <summary>
        /// Gets the hostname of the server that could not be connected to.
        /// </summary>
        public string Hostname { get; private set; }

        /// <summary>
        /// Sets the System.Runtime.Serialization.SerializationInfo with
        /// information about the exception.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains
        /// contextual information about the source or destination.
        /// </param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Hostname", Hostname);
            base.GetObjectData(info, context);
        }
    }
}

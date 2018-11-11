// <copyright file="UploadStatus.cs" company="DevDigital">
// Copyright (c) DevDigital. All rights reserved.
// </copyright>

namespace ArmTester.Storage
{
    /// <summary>
    /// Upload status.
    /// </summary>
    public class UploadStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadStatus"/> class.
        /// </summary>
        /// <param name="bytesTransferred">The bytes transferred.</param>
        /// <param name="numberOfFilesTransferred">The number of files transferred.</param>
        /// <param name="numberOfFilesFailed">The number of files failed.</param>
        /// <param name="numberOfFilesSkipped">The number of files skipped.</param>
        public UploadStatus(long bytesTransferred, long numberOfFilesTransferred, long numberOfFilesFailed, long numberOfFilesSkipped)
        {
            this.BytesTransferred = bytesTransferred;
            this.NumberOfFilesTransferred = numberOfFilesTransferred;
            this.NumberOfFilesFailed = numberOfFilesFailed;
            this.NumberOfFilesSkipped = numberOfFilesSkipped;
        }

        /// <summary>
        /// Gets the bytes transferred.
        /// </summary>
        /// <value>
        /// The bytes transferred.
        /// </value>
        public long BytesTransferred { get; }

        /// <summary>
        /// Gets the number of files transferred.
        /// </summary>
        /// <value>
        /// The number of files transferred.
        /// </value>
        public long NumberOfFilesTransferred { get; }

        /// <summary>
        /// Gets the number of files failed.
        /// </summary>
        /// <value>
        /// The number of files failed.
        /// </value>
        public long NumberOfFilesFailed { get; }

        /// <summary>
        /// Gets the number of files skipped.
        /// </summary>
        /// <value>
        /// The number of files skipped.
        /// </value>
        public long NumberOfFilesSkipped { get; }
    }
}
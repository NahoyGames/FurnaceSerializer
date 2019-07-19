using System;

namespace FurnaceSerializer
{
    public interface ISerializer
    {
        /// <summary>
        /// The type this writer supports
        /// </summary>
        Type Type { get; }
        
        /// <summary>
        /// Get the size in bytes of the value to initialize the buffer with a correct length
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>Size in bytes or -1 for error cases</returns>
        int SizeOf(object value);
        
        /// <summary>
        /// Write the value into the buffer
        /// </summary>
        /// <param name="value">Value being written</param>
        /// <param name="buffer">Buffer to write the value into</param>
        /// <param name="position">Index in the buffer to start writing, inclusive</param>
        /// <returns>Success or if buffer ran out of space</returns>
        bool Write(object value, byte[] buffer, ref int position);

        /// <summary>
        /// Read this serializer type from the buffer
        /// </summary>
        /// <param name="buffer">The read buffer</param>
        /// <param name="position">Position at which to start reading, inclusive</param>
        /// <param name="peek">Read without incrementing position</param>
        /// <returns>The read data</returns>
        object Read(byte[] buffer, ref int position, bool peek = false);
    }
}
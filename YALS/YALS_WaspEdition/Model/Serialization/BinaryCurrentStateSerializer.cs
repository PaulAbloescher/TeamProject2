// ----------------------------------------------------------------------- 
// <copyright file="BinaryCurrentStateSerializer.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the BinaryCurrentStateSerializer class.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Serialization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Represents the <see cref="BinaryCurrentStateSerializer"/> class.
    /// </summary>
    public class BinaryCurrentStateSerializer : ICurrentStateSerializer
    {
        /// <summary>
        /// The formatter of the class.
        /// </summary>
        private readonly BinaryFormatter binaryFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryCurrentStateSerializer"/> class.
        /// </summary>
        /// <param name="binder">The binder for the formatter of the class.</param>
        public BinaryCurrentStateSerializer(SerializationBinder binder)
        {
            this.binaryFormatter = new BinaryFormatter();
            this.binaryFormatter.Binder = binder ?? throw new ArgumentNullException(nameof(binder));
        }

        /// <summary>
        /// Deserializes the file at the specified path into an <see cref="CurrentState"/> object.
        /// </summary>
        /// <param name="path">The path for the file.</param>
        /// <returns>A <see cref="CurrentState"/> object.</returns>
        public CurrentState Deserialize(string path)
        {
            CurrentState state;

            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    state = (CurrentState)this.binaryFormatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    throw new InvalidOperationException("The file does not have the right format or the components that are saved in the file are not loaded in the current application.", e);
                }
                catch (InvalidCastException e)
                {
                    throw new InvalidOperationException("The file does not have the right format.", e);
                }
            }

            return state;
        }

        /// <summary>
        /// Serializes a <see cref="CurrentState"/> object to the specified output path.
        /// </summary>
        /// <param name="outputPath">The output path for the serialized object.</param>
        /// <param name="state">The state that should get serialized.</param>
        public void Serialize(string outputPath, CurrentState state)
        {
            if (outputPath == null)
            {
                throw new ArgumentNullException(nameof(outputPath));
            }

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            using (Stream stream = new FileStream(outputPath, FileMode.Create))
            {
                this.binaryFormatter.Serialize(stream, state);
            }
        }
    }
}

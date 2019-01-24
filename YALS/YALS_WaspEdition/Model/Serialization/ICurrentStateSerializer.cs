// ----------------------------------------------------------------------- 
// <copyright file="ICurrentStateSerializer.cs" company="FHWN.ac.at"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This is the ICurrentStateSerializer interface.</summary> 
// <author>Killerwasps</author> 
// -----------------------------------------------------------------------
namespace YALS_WaspEdition.Model.Serialization
{
    /// <summary>
    /// Represents the <see cref="ICurrentStateSerializer"/> class.
    /// </summary>
    public interface ICurrentStateSerializer
    {
        /// <summary>
        /// Serializes a <see cref="CurrentState"/> object to the specified output path.
        /// </summary>
        /// <param name="outputPath">The output path for the serialized object.</param>
        /// <param name="state">The state that should get serialized.</param>
        void Serialize(string outputPath, CurrentState state);

        /// <summary>
        /// Deserializes the file at the specified path into an <see cref="CurrentState"/> object.
        /// </summary>
        /// <param name="path">The path for the file.</param>
        /// <returns>A <see cref="CurrentState"/> object.</returns>
        CurrentState Deserialize(string path);
    }
}

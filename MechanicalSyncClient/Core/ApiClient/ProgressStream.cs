using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Core.ApiClient
{
    public class ProgressStream : Stream
    {
        private readonly Stream _innerStream;
        private long _totalBytesRead;
        private readonly long _totalBytes;

        public event Action<long, long> ProgressChanged;

        public override bool CanRead => _innerStream.CanRead;

        public override bool CanSeek => _innerStream.CanSeek;

        public override bool CanWrite => _innerStream.CanWrite;

        public override long Length => _innerStream.Length;

        public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

        public ProgressStream(Stream innerStream, long totalBytes)
        {
            _innerStream = innerStream;
            _totalBytes = totalBytes;
            _totalBytesRead = 0;
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = _innerStream.Read(buffer, offset, count);
            _totalBytesRead += bytesRead;
            ReportProgress();
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int bytesRead = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
            _totalBytesRead += bytesRead;
            ReportProgress();
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }

        private void ReportProgress()
        {
            ProgressChanged?.Invoke(_totalBytesRead, _totalBytes);
        }
    }
}
